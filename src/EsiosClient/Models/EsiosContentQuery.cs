using System.Collections.Generic;

namespace EsiosClient.Models;

public class EsiosContentQuery
{
    public IEnumerable<string>? TaxonomyTerms { get; set; }
    public IEnumerable<string>? Vocabularies { get; set; }
}
