using BeFit.API.Controllers.Base;
using BeFit.Application.Features.Food.Command.Create;
using BeFit.Application.Features.Food.Command.Delete;
using BeFit.Application.Features.Food.Command.Update;
using BeFit.Application.Features.Food.Query.Filter;
using BeFit.Application.Features.Food.Query.Get;
using BeFit.Application.Features.Food.Query.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace BeFit.API.Controllers;
public class NutrientController(IMediator mediator) : CustomBaseController
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]GetFoodsRequest request)
        => ControllerResponse((await mediator.Send(request)).Response);
    [HttpGet("filter")]
    public async Task<IActionResult> GetFilter([FromQuery]FilterFoodRequest request)
        => ControllerResponse((await mediator.Send(request)).Response);
    [HttpGet("{Id}")]
    public async Task<IActionResult> Get([FromRoute]GetFoodByIdRequest request)
        => ControllerResponse((await mediator.Send(request)).Response);
    [HttpPost]
    public async Task<IActionResult> Create(CreateNutrientRequest request)
        => ControllerResponse((await mediator.Send(request)).Response);
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateNutrientRequest request)
        => ControllerResponse((await mediator.Send(request)).Response);
    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete([FromRoute]DeleteNutrientRequest request)
        => ControllerResponse((await mediator.Send(request)).Response);

}