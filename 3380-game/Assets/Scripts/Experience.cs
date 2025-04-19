namespace Game.Assets.Scripts;

public sealed class Experience(uint points)
{
<<<<<<< Updated upstream
    public uint Points { get; private set; } = points;

    public Experience() : this(0)
    {
    }
    
    /// <summary>
    /// Adds the specified number of points to the total stored points
    /// </summary>
    /// <param name="points"></param>
    public void AddPoints(uint points) => Points += points;
    
    /// <summary>
    /// Converts the total number of points to a level using the inverse of our level to points formula
    /// This inverse formula is not exact given the use of ceil and floor functions in the original formula, but the difference is negligible
    /// </summary>
    /// <returns>The level associated with the number of points in this Experience object</returns>
    public int GetLevel()
    {
        var numerator = (-0.25f * double.Sqrt(Points) - 5) * (5 - 0.0005f * double.Sqrt(Points));
        var denominator = 25 - 0.00000025 * Points;
        return -(int)(numerator / denominator);
    }
}
=======
	public uint Points { get; private set; } = points;

	public Experience() : this(0)
	{
	}
	
	/// <summary>
	/// Adds the specified number of points to the total stored points
	/// </summary>
	/// <param name="points"></param>
	public void AddPoints(uint points) => Points += points;
	
	/// <summary>
	/// Converts the total number of points to a level using the inverse of our level to points formula
	/// This inverse formula is not exact given the use of ceil and floor functions in the original formula, but the difference is negligible
	/// </summary>
	/// <returns>The level associated with the number of points in this Experience object</returns>
	public int GetLevel()
	{
		var numerator = (-0.25f * double.Sqrt(Points) - 5) * (5 - 0.0005f * double.Sqrt(Points));
		var denominator = 25 - 0.00000025 * Points;
		return -(int)(numerator / denominator);
	}
}
>>>>>>> Stashed changes
