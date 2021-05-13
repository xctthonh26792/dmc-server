using Microsoft.AspNetCore.Mvc;
using Tenjin.Apis.Controllers;
using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Models.Views;
using Tenjin.Sys.Services.Interfaces;

namespace Tenjin.Sys.Apis.Controllers
{
    [ApiController]
    [Route("api/document-export")]
    public class DocumentExportController : BaseController<DocumentExport, DocumentExportView>
    {
        private readonly IDocumentExportService _service;
        public DocumentExportController(IDocumentExportService service) : base(service)
        {
            _service = service;
        }
    }
}
