using System;

namespace Snippr.Domain.Entities
{
    public class Snippet
    {
        public Guid Identifier { get; set; }
        public Author Author { get; set; }
        public string Code { get; set; }
    }
}
