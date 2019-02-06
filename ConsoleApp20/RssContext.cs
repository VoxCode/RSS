using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp20
{
    class RssContext : DbContext
    {
        public DbSet<RssNews> RssNews { get; set; }
        public DbSet<RssSource> RssSources { get; set; }
    }

    class RssNews
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string NewsUrl { get; set; }
        public int RssSourceId { get; set; }
    }

    class RssSource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public List<RssNews> RssNews { get; set; }
    }
}
