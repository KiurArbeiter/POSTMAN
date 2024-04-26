namespace teamtrack_api.Model;

public record EventPeople {
    public int Id {get; set;}
    public string? EventId {get; set;}
    public string? PersonId {get; set;}
}
