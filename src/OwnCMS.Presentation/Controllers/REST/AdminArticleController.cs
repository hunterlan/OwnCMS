using Microsoft.AspNetCore.Mvc;
using OwnCMS.Application.Features.Articles.Imports;
using OwnCMS.Presentation.Auth;

namespace OwnCMS.Presentation.Controllers.REST;

[ApiController]
[Route("/api/admin/articles")]
[ApiKeyAuth]
public class AdminArticleController(IArticleImportService articleImportService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> ImportArticleMetadata([FromBody] ArticleImportDto metadata, CancellationToken cancellationToken)
    {
        var articleId = await articleImportService.ImportMetadata(metadata.Name, metadata.Slug, metadata.Category, cancellationToken);
        
        return Ok(articleId);
    }
    
    [HttpPatch("{articleId:guid}")]
    public async Task<IActionResult> ImportArticleContent(Guid articleId, IFormFile htmlStreamContent, IFormFile cssStreamContent, CancellationToken cancellationToken)
    {
        await articleImportService.ImportContent(articleId, htmlStreamContent.OpenReadStream(), cssStreamContent.OpenReadStream(), cancellationToken);

        return NoContent();
    }
}