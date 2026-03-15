# OwnCMS progress-oriented issue draft

This document is written for you first.

The goal is not only to list work, but to make it obvious what progress you should expect to see after each issue is completed.

## Current target in one view

We are building toward this shape:

- `OwnCMS.Entities` as reusable domain package
- `OwnCMS.Application` as reusable CMS brain
- `OwnCMS.Persistence.PostgreSQL` as dedicated persistence layer
- `OwnCMS.Presentation` as read-only SSR-style site that calls Application directly
- future public `OwnCMS.Api` for reads and writes

The following decisions are already locked and are not open issues anymore:

- article to category is many-to-many
- `article_content.article_id` is required
- `category.name` is required
- HTML and CSS should be stored as `text`
- `for_old_soft_only` should not live in the shared DB/core model

## Epic 1: Finalize the database model so implementation can start cleanly

### Issue 1: Align the schema with the locked business rules

**Why this exists**

Even though the rules are decided, the actual database model still needs to be shaped to match them.

**What should be done**

- make article-to-category a true many-to-many relationship
- require `article_content.article_id`
- require `category.name`
- store HTML and CSS as `text`
- normalize naming such as `update_at` to `updated_at`
- remove `for_old_soft_only` from the shared core data model

**What progress you should expect**

When this issue is done, the project stops carrying contradictory schema assumptions.

You should expect one clear, implementation-ready data model that matches the product direction we already agreed on.

**Visible outcome**

- an updated target schema
- cleaned-up architecture notes
- no more ambiguity around core content storage

## Epic 2: Create the reusable CMS core

### Issue 2: Shape `OwnCMS.Entities` around the real domain

**Why this exists**

The reusable core cannot be stable unless the domain package is small, clean, and free from presentation-specific rules.

**What should be done**

- define the core article and category concepts
- keep presentation-only rules out of the package
- keep infrastructure concerns out of the package

**What progress you should expect**

When this issue is done, the project has a real shared domain layer instead of a vague idea of one.

You should expect better boundaries and less risk of one app polluting another.

**Visible outcome**

- a clearly scoped domain package
- no site-specific flags mixed into shared entities
- a stable base for Application

### Issue 3: Build the first public surface of `OwnCMS.Application`

**Why this exists**

This is the main “brain” of the CMS, and both Presentation and the future API depend on it.

**What should be done**

- expose clear use-case services for reads and writes
- keep CQRS internal only where it helps
- define the first article and category operations

**What progress you should expect**

When this issue is done, the architecture becomes real for the first time.

You should expect a reusable application layer that other projects can consume without needing to know database details.

**Visible outcome**

- a first usable Application contract
- read operations for public site and API
- write operations ready for admin workflows

### Issue 4: Build `OwnCMS.Persistence.PostgreSQL`

**Why this exists**

Application needs real persistence, but we do not want PostgreSQL and EF Core leaking into the reusable core packages.

**What should be done**

- add the dedicated PostgreSQL persistence project
- map the finalized schema
- connect persistence to Application contracts

**What progress you should expect**

When this issue is done, the system stops being just architecture on paper.

You should expect real reads and writes against the chosen data model.

**Visible outcome**

- dedicated persistence layer exists
- Application can talk to PostgreSQL cleanly
- package boundaries remain intact

## Epic 3: Deliver the first Presentation experience

### Issue 5: Implement Application-backed reads for `OwnCMS.Presentation`

**Why this exists**

Presentation is supposed to be a read-only site, so its read path is the first concrete app behavior we should see.

**What should be done**

- read articles
- read categories
- read article lists by category
- compose a homepage model through Application

**What progress you should expect**

When this issue is done, the public site direction becomes visible.

You should expect the Presentation app to be able to display real content without owning business logic.

**Visible outcome**

- Presentation reads through Application directly
- no write behavior leaks into the site
- reusable query surface proves useful

### Issue 6: Add the default site experience through files and configuration

**Why this exists**

The first version should work out of the box even before site-wide management is moved into the database.

**What should be done**

- define default homepage behavior
- define default design and shared styling
- define configuration-driven fallback rules
- keep `for_old_soft_only` behavior inside Presentation, not in shared core

**What progress you should expect**

When this issue is done, the product starts to feel like a real installable website instead of only a backend core.

You should expect a predictable initial site experience with minimal setup.

**Visible outcome**

- usable default homepage
- usable default design
- Presentation-specific behavior stays in Presentation

## Epic 4: Deliver the first public API

### Issue 7: Expose public read endpoints for articles and categories

**Why this exists**

You already decided the first public reads should be articles and categories.

**What should be done**

- expose article read endpoints
- expose category read endpoints
- reuse Application contracts instead of duplicating business logic

**What progress you should expect**

When this issue is done, external clients can consume the CMS for public content reads.

You should expect the API to become a real product surface, not only a future idea.

**Visible outcome**

- first public read API exists
- API stays thin
- Application remains the real source of behavior

### Issue 8: Expose the first write workflow for article creation

**Why this exists**

The first write workflow you defined is very specific: admin creates an article with related data, and missing categories are created automatically.

**What should be done**

- create article
- include related content data
- assign categories
- create categories when they do not already exist

**What progress you should expect**

When this issue is done, the CMS becomes operational, not just readable.

You should expect the first meaningful admin workflow to exist end-to-end.

**Visible outcome**

- article creation works through Application
- API or admin-facing caller can create related data in one flow
- category creation is handled automatically when needed

## Epic 5: Later-phase enhancements

### Issue 9: Add database-managed site-wide settings later

**Why this exists**

This is intentionally deferred work, not forgotten work.

**What should be done**

- move homepage definition into managed content later if needed
- move theme and shared styling into managed content later if needed
- define precedence between database values and file/config defaults

**What progress you should expect**

When this issue is done, the CMS evolves from a good default experience into a more fully managed site platform.

You should expect more flexibility, but only after the simpler first version is stable.

**Visible outcome**

- database-managed site settings exist
- fallback strategy remains clear
- the first version does not get overloaded too early

## Suggested order of delivery

1. Issue 1
2. Issue 2
3. Issue 3
4. Issue 4
5. Issue 5
6. Issue 6
7. Issue 7
8. Issue 8
9. Issue 9

## What “good progress” should look like overall

If we are moving in the right direction, you should see progress in this order:

1. the data model becomes clean and non-contradictory
2. the reusable core becomes concrete
3. Presentation starts reading real content
4. the API starts exposing real public behavior
5. later enhancements stay additive instead of forcing rework
