﻿using System.Linq.Expressions;
using System.Reflection;
using FluentIL.ExpressionParser;

// ReSharper disable CheckNamespace
namespace FluentIL
// ReSharper restore CheckNamespace
{
    partial class DynamicMethodBody
    {
        public DynamicMethodBody IfEmptyString(bool not)
        {
            FieldInfo stringEmpty = typeof (string).GetField("Empty");
            MethodInfo stringOpEqualityMethod = typeof (string).GetMethod(
                "op_Equality", new[] {typeof (string), typeof (string)});

            var emitter = new IfEmitter(this);
            _IfEmitters.Push(emitter);
            Ldsfld(stringEmpty)
                .Call(stringOpEqualityMethod);

            emitter.EmitBranch(not);
            return this;
        }

        public DynamicMethodBody IfEmptyString()
        {
            return IfEmptyString(false);
        }

        public DynamicMethodBody IfNotEmptyString()
        {
            return IfEmptyString(true);
        }

        public DynamicMethodBody IfNull(bool not)
        {
            var emitter = new IfEmitter(this);
            _IfEmitters.Push(emitter);
            emitter.EmitBranch(!not);
            return this;
        }

        public DynamicMethodBody IfNull()
        {
            return IfNull(false);
        }

        public DynamicMethodBody IfNotNull()
        {
            return IfNull(true);
        }

        public DynamicMethodBody If(Expression expression)
        {
            var emitter = new IfEmitter(this);
            _IfEmitters.Push(emitter);
            Expression(expression);
            emitter.EmitBranch(false);
            return this;
        }

        public DynamicMethodBody If(string expression)
        {
            var emitter = new IfEmitter(this);
            _IfEmitters.Push(emitter);
            Parser.Parse(expression, this);
            emitter.EmitBranch(false);
            return this;
        }
    }
}