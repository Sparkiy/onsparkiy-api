namespace onsparkiy.api.Models.Contracts
{
	/// <summary>
	/// User profile contract.
	/// </summary>
	public interface IUserProfile
	{
		/// <summary>
		/// Gets or sets the profile picture.
		/// </summary>
		/// <value>
		/// The profile picture.
		/// </value>
		string Picture { get; set; }

		/// <summary>
		/// Gets or sets the cover.
		/// </summary>
		/// <value>
		/// The cover.
		/// </value>
		string Cover { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		string Description { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instances email is public.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instances email is public; otherwise, <c>false</c>.
		/// </value>
		bool IsEmailPublic { get; set; }

		/// <summary>
		/// Gets or sets the user.
		/// </summary>
		/// <value>
		/// The user.
		/// </value>
		User User { get; set; }
	}
}