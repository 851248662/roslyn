﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Test.Utilities;
using Xunit;

namespace Microsoft.CodeAnalysis.Editor.CSharp.UnitTests.ConvertSwitchStatementToExpression
{
    public partial class ConvertSwitchStatementToExpressionTests
    {
        [Fact, Trait(Traits.Feature, Traits.Features.CodeActionsConvertSwitchStatementToExpression)]
        public async Task TestNested()
        {
            await TestInRegularAndScriptAsync(
@"class Program
{    
    int M(int i, int j)
    {
        int r;
        {|FixAllInDocument:switch|} (i)
        {
            case 1:
                r = 1;
                break;
            case 2:
                r = 2;
                break;
            case 3:
                r = 3;
                break;
            default:
                r = 4;
                break;
        }
        int x, y;
        switch (i)
        {
            case 1:
                x = 1;
                y = 1;
                break;
            case 2:
                x = 1;
                y = 1;
                break;
            case 3:
                x = 1;
                y = 1;
                break;
            default:
                x = 1;
                y = 1;
                break;
        }
        switch (i)
        {
            default:
                throw null;
            case 1:
                switch (j)
                {
                    case 10:
                        return 10;
                    case 20:
                        return 20;
                    case 30:
                        return 30;
                }
                return 0;
            case 2:
                switch (j)
                {
                    case 10:
                        return 10;
                    case 20:
                        return 20;
                    case 30:
                        return 30;
                    case var _:
                        return 0;
                }
            case 3:
                switch (j)
                {
                    case 10:
                        return 10;
                    case 20:
                        return 20;
                    case 30:
                        return 30;
                    case var v:
                        return 0;
                }
        }
    }
}",
@"class Program
{    
    int M(int i, int j)
    {
        var r = i switch
        {
            1 => 1,
            2 => 2,
            3 => 3,
            _ => 4
        };
        var (x, y) = i switch
        {
            1 => (1, 1),
            2 => (1, 1),
            3 => (1, 1),
            _ => (1, 1)
        };
        return i switch
        {
            1 => j switch
            {
                10 => 10,
                20 => 20,
                30 => 30,
                _ => 0
            },
            2 => j switch
            {
                10 => 10,
                20 => 20,
                30 => 30,
                var _ => 0
            },
            3 => j switch
            {
                10 => 10,
                20 => 20,
                30 => 30,
                var v => 0
            },
            _ => throw null
        };
    }
}");
        }

        [Fact, Trait(Traits.Feature, Traits.Features.CodeActionsConvertSwitchStatementToExpression)]
        public async Task TestNested2()
        {
            await TestInRegularAndScriptAsync(
@"class Program
{    
    int M(int i, int j)
    {
        int r;
        switch (i)
        {
            case 1:
                r = 1;
                break;
            case 2:
                r = 2;
                break;
            case 3:
                r = 3;
                break;
            default:
                r = 4;
                break;
        }
        int x, y;
        switch (i)
        {
            case 1:
                x = 1;
                y = 1;
                break;
            case 2:
                x = 1;
                y = 1;
                break;
            case 3:
                x = 1;
                y = 1;
                break;
            default:
                x = 1;
                y = 1;
                break;
        }
        switch (i)
        {
            default:
                throw null;
            case 1:
                switch (j)
                {
                    case 10:
                        return 10;
                    case 20:
                        return 20;
                    case 30:
                        return 30;
                }
                return 0;
            case 2:
                {|FixAllInDocument:switch|} (j)
                {
                    case 10:
                        return 10;
                    case 20:
                        return 20;
                    case 30:
                        return 30;
                    case var _:
                        return 0;
                }
            case 3:
                switch (j)
                {
                    case 10:
                        return 10;
                    case 20:
                        return 20;
                    case 30:
                        return 30;
                    case var v:
                        return 0;
                }
        }
    }
}",
@"class Program
{    
    int M(int i, int j)
    {
        var r = i switch
        {
            1 => 1,
            2 => 2,
            3 => 3,
            _ => 4
        };
        var (x, y) = i switch
        {
            1 => (1, 1),
            2 => (1, 1),
            3 => (1, 1),
            _ => (1, 1)
        };
        return i switch
        {
            1 => j switch
            {
                10 => 10,
                20 => 20,
                30 => 30,
                _ => 0
            },
            2 => j switch
            {
                10 => 10,
                20 => 20,
                30 => 30,
                var _ => 0
            },
            3 => j switch
            {
                10 => 10,
                20 => 20,
                30 => 30,
                var v => 0
            },
            _ => throw null
        };
    }
}");
        }
    }
}
