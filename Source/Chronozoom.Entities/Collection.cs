﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Chronozoom.Entities
{
    /// <summary>
    /// Represents a collection of timelines.
    /// </summary>
    [DataContract]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
    public class Collection
    {
        /// <summary>
        /// Constructor used to set default values.
        /// </summary>
        public Collection()
        {
            this.Id                 = Guid.NewGuid();   // Don't use [DatabaseGenerated(DatabaseGeneratedOption.Identity)] on Id
            this.Default            = false;
            this.MembersAllowed     = false;
            this.PubliclySearchable = false;
        }

        /// <summary>
        /// The ID of the collection.
        /// </summary>
        [Key]
        [DataMember]
        public Guid Id { get; set; }

        /// <summary>
        /// Indicates if the collection is the default collection for the indicated supercollection.
        /// There should be only one default collection per supercollection.
        /// </summary>
        [DataMember]
        [Column(TypeName = "bit")]
        public bool Default { get; set; }

        /// <summary>
        /// The title of the collection.
        /// </summary>
        [DataMember]
        [Required]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar")]
        public string Title { get; set; }

        /// <summary>
        /// URL-sanitized version of title, which will be used as part of a URL path.
        /// Only a-z, 0-9 and hyphen are allowed. This should be unique per supercollection.
        /// </summary>
        [DataMember]
        [Required]
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string Path { get; set; }

        /// <summary>
        /// The user ID for the collection owner.
        /// </summary>
        [DataMember]
        [Required]
        public User User { get; set; }

        /// <summary>
        /// The theme (i.e. space, blue, etc) associated to this collection.
        /// </summary>
        [DataMember(Name="theme")]
        //[DataMember]
        public string Theme { get; set; }

        /// <summary>SuperCollection for this collection</summary>
        [DataMember]
        [Required]
        public SuperCollection SuperCollection { get; set; }

        /// <summary>
        /// On/Off switch for permitting a list of members to edit this collection.
        /// </summary>
        [DataMember]
        [Column(TypeName = "bit")]
        public bool MembersAllowed { get; set; }

        /// <summary>
        /// A list of users who have special rights to the collection, other than the collection owner.
        /// </summary>
        [DataMember]
        public virtual Collection<Member> Members { get; set; }

        /// <summary>
        /// On/Off switch for allowing the collection to appear in search results for
        /// people who do not have edit rights and are not currently viewing the collection.
        /// </summary>
        [DataMember]
        [Column(TypeName = "bit")]
        public bool PubliclySearchable { get; set; }
    }

    /// <summary>
    /// The Collection.Theme text field is actually a compressed JSON object comprising of the following.
    /// </summary>
    public class Theme
    {
        public string backgroundUrl         { get; set; }
        public string backgroundColor       { get; set; }
        public string timelineColor         { get; set; }
        public string timelineStrokeStyle   { get; set; }
        public string infoDotfillColor      { get; set; }
        public string infoDotBorderColor    { get; set; }
        public Boolean kioskMode            { get; set; }
    }
}
