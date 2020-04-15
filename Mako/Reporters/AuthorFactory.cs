using Content.Api;
using Mako.Reporters.Entities;

namespace Mako.Reporters
{
    public static class AuthorFactory
    {
        public static Author Create(Reporter reporter)
        {
            return new Author(
                reporter.Details.Name,
                reporter.Details.Image);
        }
    }
}