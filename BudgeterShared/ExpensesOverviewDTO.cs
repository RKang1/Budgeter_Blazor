﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BudgeterShared
{
    class ExpensesOverviewDTO
    {
        public Decimal Income { get; set; }

        public Decimal Savings { get; set; }

        public Decimal Needs { get; set; }

        public Decimal Wants { get; set; }
    }
}
