﻿namespace TestHelpers;

public static class TestValues
{
    public const string ValidIban = "AD1200012030200359100100";
    public const string ValidIbanPartitioned = "AD12 0001 2030 2003 5910 0100";
    public const string ValidIbanPartitionedAndWithLowercase = "ad12 0001 2030 2003 5910 0100";
    public const string InvalidIban = "__INVALID_IBAN";
    public const string IbanForCustomRuleFailure = "CUSTOMRULEFAILURE";
    public const string IbanForCustomRuleException = "CUSTOMRULEEXCEPTION";
}
