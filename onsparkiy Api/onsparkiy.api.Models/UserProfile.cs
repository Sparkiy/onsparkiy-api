using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using onsparkiy.api.Models.Contracts;

namespace onsparkiy.api.Models
{
	/// <summary>
	/// User profile model.
	/// </summary>
	public class UserProfile : IUserProfile
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		[Key]
		public string Id { get; set; }

		/// <summary>
		/// Gets or sets the profile picture.
		/// </summary>
		/// <value>
		/// The profile picture.
		/// </value>
		public string Picture { get; set; }

		/// <summary>
		/// Gets or sets the cover.
		/// </summary>
		/// <value>
		/// The cover.
		/// </value>
		public string Cover { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public string Description { get; set; } 

		/// <summary>
		/// Gets or sets a value indicating whether this instances email is public.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instances email is public; otherwise, <c>false</c>.
		/// </value>
		public bool IsEmailPublic { get; set; }

		/// <summary>
		/// Gets or sets the user.
		/// </summary>
		/// <value>
		/// The user.
		/// </value>
		[Required]
		public virtual User User { get; set; }

		/// <summary>
		/// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
		/// <returns>
		///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;

			return Equals((UserProfile) obj);
		}

		/// <summary>
		/// Determines whether the specified <see cref="UserProfile"/>, is equal to this instance.
		/// </summary>
		/// <param name="other">The <see cref="UserProfile"/> to compare with this instance.</param>
		/// <returns>
		///   <c>true</c> if the specified <see cref="UserProfile" /> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		protected bool Equals(UserProfile other)
		{
			return string.Equals(Id, other.Id);
		}

		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
		/// </returns>
		public override int GetHashCode()
		{
			return Id?.GetHashCode() ?? 0;
		}
	}
}