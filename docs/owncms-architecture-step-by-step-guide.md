# OwnCMS step-by-step architecture guide

This document is written for both of us, but primarily as an execution guide for future implementation work.

It should answer one practical question:

What should be built, in what order, under which constraints, so the project stays aligned with the decisions already made?

## 1. Locked decisions

Treat these as baseline constraints, not open questions:

### Package and project boundaries

- `OwnCMS.Entities` is a reusable domain package
- `OwnCMS.Application` is a reusable CMS brain package
- `OwnCMS.Persistence.PostgreSQL` is separate from the reusable core
- `OwnCMS.Presentation` is read-only and calls Application directly
- future `OwnCMS.Api` is public and uses Application for reads and writes

### Interaction style

- public consumers should see explicit use-case services
- CQRS can exist internally where it helps organization
- consumers should not be forced to know mediator-style internals

### Current schema rules

- article to category is many-to-many
- `article_content.article_id` is required
- `category.name` is required
- article HTML is stored as `text`
- article CSS is stored as `text`

### Product scope rules

- initial public read endpoints are articles and categories
- initial write workflow is article creation with related data
- if a category does not exist during article creation, create it
- `for_old_soft_only` is not part of shared core data
- `for_old_soft_only` belongs in `OwnCMS.Presentation` behavior if still needed
- site-wide homepage and design should start from files and configuration, not database-managed settings

## 2. What not to do

Guardrails matter as much as build order.

Do not do the following:

- do not keep site-specific fields in shared domain or application models
- do not let Presentation implement business logic
- do not let API duplicate business logic that belongs in Application
- do not mix EF Core concerns into `OwnCMS.Entities`
- do not over-split packages by feature too early
- do not move site-wide settings into the database in the first implementation phase

## 3. Mental model for the solution

Use this runtime picture:

- `Entities` defines domain concepts and value rules
- `Application` defines operations and orchestration
- `Persistence.PostgreSQL` makes Application stateful
- `Presentation` consumes Application for server-side reads
- `Api` consumes Application for public reads and writes

The center of gravity is `OwnCMS.Application`.

If a piece of behavior is important to multiple consumers, it should probably live there.

## 4. Data model targets

Implementation should move the database toward this shape:

### Article

- core metadata only
- no `for_old_soft_only`
- naming normalized

### Article content

- required link to article
- HTML as `text`
- CSS as `text`

### Category

- required name

### Article-category relationship

- true many-to-many
- no accidental uniqueness that blocks multiple categories per article

## 5. Presentation-specific targeting rule

Current decision:

- `for_old_soft_only` should not stay in shared DB/core design

Practical implication:

- if that rule is still needed, implement it as a Presentation concern
- likely source: configuration, local content rule, or presentation-specific filtering

Important future trigger:

- if content targeting expands beyond one SSR site, revisit this and replace it with a general targeting model
- do not reintroduce a one-off flag into shared core unless the requirement becomes truly domain-wide

## 6. Build order

Follow this order unless new information forces a change.

### Step 1: finalize schema intent in documentation and model assumptions

Before implementation starts:

- ensure the target schema reflects the locked rules
- ensure the docs do not contain stale contradictions
- ensure shared-core versus Presentation-only responsibilities are explicit

Done means:

- no unresolved ambiguity about categories, content storage, required fields, or `for_old_soft_only`

### Step 2: shape `OwnCMS.Entities`

Focus:

- articles
- categories
- content-related value concepts

Keep out:

- EF Core
- HTTP
- Presentation-only filtering rules

Done means:

- shared domain concepts are stable and app-agnostic

### Step 3: shape `OwnCMS.Application`

Focus:

- public use-case services for reads
- public use-case services for writes
- internal orchestration
- optional internal CQRS organization

First operations to support:

- read articles
- read categories
- create article with related data
- create category if missing during article creation

Done means:

- both Presentation and future API have a usable, shared application surface

### Step 4: build `OwnCMS.Persistence.PostgreSQL`

Focus:

- EF Core mappings
- database access
- connecting Application contracts to PostgreSQL

Done means:

- Application can read and write real data without violating package boundaries

### Step 5: wire `OwnCMS.Presentation`

Focus:

- read-only flows
- article rendering
- category rendering
- homepage composition
- file/config defaults for homepage and design

Keep out:

- write flows
- shared-core targeting flags

Done means:

- Presentation reads real data through Application and still works with default site configuration

### Step 6: wire future `OwnCMS.Api`

Focus:

- public article reads
- public category reads
- first article creation workflow

Done means:

- the public API is thin, useful, and backed by Application instead of duplicate logic

### Step 7: defer later enhancements

Later only:

- database-managed homepage
- database-managed themes
- database-managed shared CSS
- generalized multi-site or audience targeting

Done means:

- first version stays small enough to finish

## 7. First workflow that matters most

The first meaningful write workflow is:

1. admin submits article data
2. system validates required fields
3. system ensures article content exists and is linked
4. system checks requested categories
5. missing categories are created
6. article-category links are created
7. article is available for future reads

This workflow should shape early Application and API design more than abstract patterns should.

## 8. Delivery checkpoints

Use these checkpoints to judge progress:

### Checkpoint A: model clarity

- schema contradictions removed
- site-specific rule moved out of shared core

### Checkpoint B: reusable core exists

- Entities and Application boundaries are real
- persistence is separate

### Checkpoint C: Presentation can read

- articles and categories can be rendered through Application
- default site experience works without DB-managed site settings

### Checkpoint D: API can read and write

- public article and category reads exist
- article creation flow works end-to-end

## 9. Known deferred work

These are not current blockers, but they should remain visible:

- database-managed site-wide settings
- generalized content targeting beyond one site
- broader public API scope beyond articles and categories
- richer admin workflows beyond initial article creation

## 10. Final reminder

If a future change is specific to one site, keep pressure against putting it into shared core packages.

If a future change is truly reusable across Presentation, API, and possible future consumers, prefer moving it into Application and backing it with the persistence layer.
