public class Note
{
    public string? Id { get; set; }
    public string Title { get; set; }
    public string Type { get; set; }
    public List<string> Tags { get; set; }
    public List<string> Backlinks { get; set; }
    public string Content { get; set; }

    public string ToMarkDown()
    {
        var frontmatter =
            $"---\n" +
            $"title: {Title}\n" +
            $"type: {Type}\n" +
            $"tags: {Tags}\n" +
            $"Backlinks: {Backlinks}\n" +
            $"---\n\n";
        return frontmatter + Content;
    }
}
