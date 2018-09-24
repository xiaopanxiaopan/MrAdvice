﻿#region Mr. Advice
// Mr. Advice
// A simple post build weaving package
// http://mradvice.arxone.com/
// Released under MIT license http://opensource.org/licenses/mit-license.php
#endregion
namespace ArxOne.MrAdvice.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Annotation;

    /// <summary>
    /// Extensions to type
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Enumerates from type to topmost parent
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static IEnumerable<Type> GetSelfAndParents(this Type type)
        {
            while (type != null)
            {
                yield return type;
                type = type.GetInformationReader().BaseType;
            }
        }

        /// <summary>
        /// Gets the self and enclosing types.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static IEnumerable<Type> GetSelfAndEnclosing(this Type type)
        {
            while (type != null)
            {
                yield return type;
                type = type.DeclaringType;
            }
        }

        /// <summary>
        /// Gets the fields and properties.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="bindingFlags">The binding flags.</param>
        /// <returns></returns>
        public static IEnumerable<MemberInfo> GetFieldsAndProperties(this Type type, BindingFlags bindingFlags)
        {
            return type.GetMembersReader().GetFields(bindingFlags).Cast<MemberInfo>()
                .Concat(type.GetMembersReader().GetProperties(bindingFlags));
        }

        /// <summary>
        /// Gets the member attributes for the given type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static VisibilityScope GetVisibilityScope(this Type type)
        {
            return type.GetInformationReader().Attributes.ToVisibilityScope();
        }

        /// <summary>
        /// Converts the type attributes to member attributes.
        /// </summary>
        /// <param name="typeAttributes">The type attributes.</param>
        /// <returns></returns>
        public static VisibilityScope ToVisibilityScope(this TypeAttributes typeAttributes)
        {
            switch (typeAttributes & TypeAttributes.VisibilityMask)
            {
                case TypeAttributes.NotPublic: // 0
                    return VisibilityScope.PrivateGlobalType;
                case TypeAttributes.Public: // 1
                    return VisibilityScope.PublicGlobalType;
                case TypeAttributes.NestedPublic: // 2
                    return VisibilityScope.PublicNestedType;
                case TypeAttributes.NestedPrivate: // 3
                    return VisibilityScope.PrivateNestedType;
                case TypeAttributes.NestedFamily: // 4
                    return VisibilityScope.FamilyNestedType;
                case TypeAttributes.NestedAssembly: // 5
                    return VisibilityScope.AssemblyNestedType;
                case TypeAttributes.NestedFamANDAssem: // 6
                    return VisibilityScope.FamilyAndAssemblyType;
                case TypeAttributes.NestedFamORAssem: // 7
                    return VisibilityScope.FamilyOrAssemblyType;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
