﻿using BeFit.API.Controllers.Base;
using BeFit.Application.Features.Post.Commands.Create;
using BeFit.Application.Features.Post.Commands.Delete;
using BeFit.Application.Features.Post.Commands.Update;
using BeFit.Application.Features.Post.Queries.Get;
using BeFit.Application.Features.Post.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BeFit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController(IMediator mediator) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]GetPostsRequest request)
            => ControllerResponse((await mediator.Send(request)).Posts);

        [HttpGet("id")]
        public async Task<IActionResult> GetById([FromQuery]GetPostByIdRequest request)
            => ControllerResponse((await mediator.Send(request)).Response);

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreatePostRequest request)
            => ControllerResponse((await mediator.Send(request)).Response);

        [HttpPut]
        public async Task<IActionResult> Update(UpdatePostRequest request)
            => ControllerResponse((await mediator.Send(request)).Response);

        [HttpDelete]
        public async Task<IActionResult> Delete(DeletePostRequest request)
            => ControllerResponse((await mediator.Send(request)).Response);
    }
}