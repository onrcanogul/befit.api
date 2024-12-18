﻿using AutoMapper;
using BeFit.Application.Common;
using BeFit.Application.DataTransferObjects;
using BeFit.Application.Repositories;
using BeFit.Application.Services.Post;
using BeFit.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BeFit.Persistence.Services.Post
{
    public class PostLikeService(IRepository<PostLike> repository, IMapper mapper, IUnitOfWork uow) : IPostLikeService
    {
        public async Task<ServiceResponse<List<PostLikeDto>>> GetByPost(Guid postId)
        {
            var likes = await repository.GetQueryable().Where(x => x.PostId == postId)
                .Include(x => x.User)
                .ToListAsync();
            var dto = mapper.Map<List<PostLikeDto>>(likes);
            return ServiceResponse<List<PostLikeDto>>.Success(dto, StatusCodes.Status200OK);
        }

        public async Task<ServiceResponse<NoContent>> Like(Guid postId, string userId)
        {
            var like = await repository.GetQueryable().Where(x => x.PostId == postId && x.UserId == userId).FirstOrDefaultAsync();

            if (like == null)
                await repository.CreateAsync(new() { UserId = userId, PostId = postId });
            else
                repository.Delete(like);
            await uow.SaveChangesAsync();
            return ServiceResponse<NoContent>.Success(StatusCodes.Status201Created);
        }
    }
}
