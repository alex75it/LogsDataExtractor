﻿module LogsDataExtractor.Core.Entities

type LogLevel =
    | Debug = 0
    | Info  = 1
    | Warn  = 2
    | Error = 3
    | Fatal = 4

type Record = {
    Date: System.DateTime
    Level: LogLevel
    Message: string
} 

