using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamWeddsManager.Application.Enums
{
    public enum ImagePath : byte
    {
        [Description(@"images/wedding")]
        Wedding,

        [Description(@"images/blog")]
        Blog,

        [Description(@"images/Template")]
        Template,

        [Description(@"images/profile")]
        ProfilePicture,

        [Description(@"documents")]
        Document
    }
}
