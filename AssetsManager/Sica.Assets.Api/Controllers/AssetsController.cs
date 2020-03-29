using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sica.Assets.Borders.Dtos.Assets;
using Sica.Assets.Borders.Entities;
using Sica.Assets.Borders.UseCases.Assets;
using Sica.Assets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sica.Assets.Controllers
{
    [ApiController]
    [Route("api/assets")]
    public class AssetsController : Controller
    {
        private readonly IActionResultConverter actionResultConverter;
        private readonly ICreateAssetUseCase createAssetUseCase;
        private readonly IGetAssetUseCase getAssetUseCase;
        private readonly IListAssetsUseCase listAssetsUseCase;
        private readonly IUpdateAssetUseCase updateAssetUseCase;
        private readonly IDeleteAssetUseCase deleteAssetUseCase;

        public AssetsController(IActionResultConverter actionResultConverter,
            ICreateAssetUseCase createAssetUseCase,
            IListAssetsUseCase listAssetsUseCase,
            IGetAssetUseCase getAssetUseCase,
            IUpdateAssetUseCase updateAssetUseCase,
            IDeleteAssetUseCase deleteAssetUseCase)
        {
            this.actionResultConverter = actionResultConverter;
            this.createAssetUseCase = createAssetUseCase;
            this.getAssetUseCase = getAssetUseCase;
            this.listAssetsUseCase = listAssetsUseCase;
            this.updateAssetUseCase = updateAssetUseCase;
            this.deleteAssetUseCase = deleteAssetUseCase;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Asset))]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await getAssetUseCase.Execute(id);
            return actionResultConverter.Convert(response);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Asset))]
        public async Task<IActionResult> Create(CreateAssetRequest asset)
        {
            var response = await createAssetUseCase.Execute(asset);
            return actionResultConverter.Convert(response);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Asset>))]
        public async Task<IActionResult> Assets()
        {
            var response = await listAssetsUseCase.Execute();
            var assets = response.Result as List<Asset>;
            Response.Headers.Add("X-Total-Count", assets.Count.ToString());

            return actionResultConverter.Convert(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(Asset))]
        public async Task<IActionResult> Update(Guid id, CreateAssetRequest assetUpdated)
        {
            var asset = assetUpdated.ToAsset(id);
            var response = await updateAssetUseCase.Execute(asset);
            return actionResultConverter.Convert(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(Asset))]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await deleteAssetUseCase.Execute(id);
            return actionResultConverter.Convert(response);
        }
    }
}
