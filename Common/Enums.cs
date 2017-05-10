using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{

    public enum Gender
    {
        Male = 0,
        Female = 1
    }

    public enum EducationLevel
    {
        Higher = 0,
        Bachelor = 1,
        Master = 2,
        IncompleteHigher = 3
    }

    public enum EnglishLevel
    {
        Beginner = 0,
        Elementary = 1,
        PreIntermediate = 2,
        Intermediate = 3,
        UpperIntermediate = 4,
        Advanced = 5
    }

    public enum SkillsLevel
    {
        Junior = 0,
        Middle = 1,
        Senior = 2
    }

    public enum CourseResponseStatus
    {
        Initial = 0,
        Viewed = 1,
        Refinement = 2,
        Accepted = 3,
        Declined = 4
    }
}
