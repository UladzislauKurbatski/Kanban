using System;
using System.Collections.Generic;
using System.Text;

namespace Kanban.BusinessLogic.Infrastructure
{
    internal static class Constant
    {
        internal static class Validation
        {
            public static string InvalidArgument(string argName) => $"Invalid argument value '{argName}'";

            public static string ShouldBeUnique(string propName) => $"Value of '{propName}' property should be unique";

            public static string TextLengthEqualOrLessThan(string propName, int length) => $"The length of the property '{propName}' should be equal or less than {length}";

            public static string CantBeNullOrEmpty(string propName) => $"The value of the property {propName} can't be null or empty.";

            internal static class Task
            {
                public static string InvalidDateTime = "Creation time of task can't be less that current time.";
            }

            internal static class User
            {
                public static string InvalidLoginOrPassword = "Invalid login or password";
            }

        }
    }
}
