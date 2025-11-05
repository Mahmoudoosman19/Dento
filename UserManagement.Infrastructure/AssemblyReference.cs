using System.Reflection;

namespace UserManagement.Infrastructure
{
    public class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}

