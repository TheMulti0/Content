using Content.Api;
using Mako.N12Reports.Entities;

namespace Mako.N12Reports
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