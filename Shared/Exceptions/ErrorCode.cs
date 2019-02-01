using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Exceptions
{
    public enum ErrorCode
    {
        INVALID_DATA = 1000,
        MISSING_BODY_DATA = 1001,
        INVALID_BIRTHDAY = 1002,
        INVALID_IDNUMBER = 1003,
        INVALID_CONVERSATION_ID = 1007,
        DuplicateEmail = 1008,

        TOKEN_REQUIRED = 2001,
        TOKEN_INVALID = 2002,

        PHONE_NOT_VALID = 3001,
        USER_EXISTED = 3002,

        USER_NOT_FOUND = 4041,
        MISSING_CUSTOMER_NAME = 1003,
        MISSING_CUSTOMER_PHONE = 1004,
        MISSING_CUSTOMER_CONTENT = 1005,
        MISSING_CONVERSATION_ID = 1006,
        MISSING_LASTMESS_ID = 1008,
    }
}
