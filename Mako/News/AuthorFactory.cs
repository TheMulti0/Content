﻿using Content.Api;

namespace Mako.News
{
    public static class AuthorFactory
    {
        public static Author Create()
        {
            return new Author(
                "החדשות 12",
                "https://rcs.mako.co.il/images/svg/news/logo-n-12.svg");
        } 
    }
}