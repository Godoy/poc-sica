﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Sica.Assets.Shared.Models
{
    public class ErrorMessage
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public ErrorMessage()
        {
        }

        public ErrorMessage(string code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
