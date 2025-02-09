using FluentResults;

namespace votingSystem.Api.Domain;

public class Candidate
{
    public int Id { get; private set; }
    public string Name { get; private set; }

    private Candidate(string name)
    {
        Name = name;
    }

    public static Result<Candidate> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Fail<Candidate>("Name cannot be null or empty");
        }

        return Result.Ok(new Candidate(name));
    }
}