﻿using Microsoft.AspNetCore.Mvc;
using Tenjin.Apis.Controllers;
using Tenjin.Sys.Models.Entities;
using Tenjin.Sys.Models.Views;
using Tenjin.Sys.Services.Interfaces;

namespace Tenjin.Sys.Apis.Controllers
{
    [ApiController]
    [Route("api/document-import")]
    public class DocumentImportController : BaseController<DocumentImport, DocumentImportView>
    {
        private readonly IDocumentImportService _service;
        public DocumentImportController(IDocumentImportService service) : base(service)
        {
            _service = service;
        }
    }
}
