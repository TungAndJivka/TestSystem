﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestSystem.Web
{
    public class MappingSettings : Profile
    {
        public MappingSettings()
        {
            //this.CreateMap<PostDto, PostViewModel>()
            //       .ForMember(x => x.Author, options => options.MapFrom(x => x.Author.Email))
            //       .ReverseMap();

            //this.CreateMap<CommentDto, CommentViewModel>()
            //       .ForMember(x => x.Author, options => options.MapFrom(x => x.Author.Email))
            //       .ReverseMap();

            //this.CreateMap<PostViewModel, PostDto>(MemberList.Source);
            //this.CreateMap<PostDto, Post>(MemberList.Source);
            //this.CreateMap<CommentDto, Comment>(MemberList.Source);

        }
    }
}
