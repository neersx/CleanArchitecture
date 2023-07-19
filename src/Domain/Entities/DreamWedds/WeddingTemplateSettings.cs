using BlazorHero.CleanArchitecture.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Domain.Entities.DreamWedds
{
    public class WeddingTemplateSettings : AuditableEntity<int>
    {
        public string BackgroundImageResolution { get; set; }
        public bool IsMultiplePage { get; set; } = false;
        public bool ContainsFooter { get; set; } = false;
        public string CopyRightText { get; set; }
        public bool EnableNotification { get; set; } = false;
        public virtual BannerSettings BannerSettings { get; set; }
        public virtual CoupleSettings CoupleSettings { get; set; }
        public virtual BrideMaidSettings BrideMaidSettings { get; set; }
        public virtual GroomsMenSettings GroomsMenSettings { get; set; }
        public virtual BestManSettings BestManSettings { get; set; }
        public virtual TimeLineSettings TimeLineSettings { get; set; }
        public virtual InvitationSettings InvitationSettings { get; set; }
        public virtual VenueSettings VenueSettings { get; set; }
        public virtual EventsSettings EventsSettings { get; set; }
        public virtual GiftRegistrySettings GiftRegistrySettings { get; set; }
        public virtual GallerySettings GallerySettings { get; set; }
        public virtual RsvpSettings RsvpSettings { get; set; }
    }

    public class BannerSettings : AuditableEntity<int>
    {
        public int Sequence { get; set; }
        [MaxLength(25)]
        public string Placement { get; set; } // Top, Bottom, Left, Right
        public bool IsBackgroundImage { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        [MaxLength(25)]
        public string DefaultResolution { get; set; } // A X B
        [MaxLength(25)]
        public string MaxResolution { get; set; }
        public bool IsCarousel { get; set; }
        public int MaxImages { get; set; }
        [MaxLength(25)]
        public string CssStyle { get; set; }
    }

    public class CoupleSettings : AuditableEntity<int>
    {
        public int Sequence { get; set; }
        public bool IsBackgroundImage { get; set; }
        [MaxLength(25)]
        public string BrideImageResolution { get; set; }
        [MaxLength(25)]
        public string GroomImageResolution { get; set; }
        [MaxLength(25)]
        public string Placement { get; set; } // Top, Bottom, Left, Right
        public bool DisplayDescription { get; set; }
        [MaxLength(500)]
        public string CssStyle { get; set; }
    }

    public class BrideMaidSettings : AuditableEntity<int>
    {
        public int Sequence { get; set; }
        public bool IsBackgroundImage { get; set; }
        [MaxLength(25)]
        public string ImageResolution { get; set; }
        public bool ContainsSocialBio { get; set; }
    }

    public class GroomsMenSettings : AuditableEntity<int>
    {
        public int Sequence { get; set; }
        public bool IsBackgroundImage { get; set; }
        [MaxLength(25)]
        public string ImageResolution { get; set; }
        public bool ContainsSocialBio { get; set; }
    }

    public class BestManSettings : AuditableEntity<int>
    {
        public int Sequence { get; set; }
        public bool IsBackgroundImage { get; set; }
        [MaxLength(25)]
        public string ImageResolution { get; set; }
        public bool ContainsSocialBio { get; set; }
        public bool ContainImage { get; set; }
        public bool ContainText { get; set; }
    }

    public class TimeLineSettings : AuditableEntity<int>
    {
        public int Sequence { get; set; }
        public bool IsBackgroundImage { get; set; }
        public bool IsSingleSide { get; set; }
        [MaxLength(25)]
        public string DefaultResolution { get; set; }
        [MaxLength(25)]
        public string MaxResolution { get; set; }
        public bool ContainsEndTitle { get; set; }
    }

    public class InvitationSettings : AuditableEntity<int>
    {
        public int Sequence { get; set; }
        public bool IsBackgroundImage { get; set; }
        [MaxLength(25)]
        public string BackgroundColor { get; set; }
        public bool CanSendEmail { get; set; }
    }

    public class VenueSettings : AuditableEntity<int>
    {
        public int Sequence { get; set; }
        public bool IsBackgroundImage { get; set; }
        public int LocationMapEnabled { get; set; }
    }

    public class EventsSettings : AuditableEntity<int>
    {
        public int Sequence { get; set; }
        public bool IsBackgroundImage { get; set; }
        public int DefaultEventsCount { get; set; }
        public int MaxEventsCount { get; set; }
    }

    public class GiftRegistrySettings : AuditableEntity<int>
    {
        public int Sequence { get; set; }
        public bool IsBackgroundImage { get; set; }

    }

    public class GallerySettings : AuditableEntity<int>
    {
        public int Sequence { get; set; }
        public bool IsBackgroundImage { get; set; }
        public string ImageResolution { get; set; }
        public string MaxImageResolution { get; set; }
        public string DefaultImagesCount { get; set; }
        public string MaxImagesCount { get; set; }
    }

    public class RsvpSettings : AuditableEntity<int>
    {
        public int Sequence { get; set; }
        public bool IsBackgroundImage { get; set; }
        public bool CanUploadGuestList { get; set; }
    }

}
