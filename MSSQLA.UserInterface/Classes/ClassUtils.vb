Public Class ClassUtils
    Public Const SQL_KEYWORDS As String = "add alter as authorization backup begin bigint binary bit break browse bulk by cascade case catch check checkpoint close clustered column commit compute constraint containstable continue create current cursor database date datetime datetime2 datetimeoffset dbcc deallocate decimal declare default delete deny desc disk distinct distributed double drop dump else end errlvl escape except exec execute exit external fetch file fillfactor float for foreign freetext freetexttable from full function goto grant group having hierarchyid holdlock identity identity_insert identitycol if image index insert int intersect into key kill lineno load merge money national nchar nocheck nocount nolock nonclustered ntext numeric nvarchar of off offsets on open opendatasource openquery openrowset openxml option order over percent plan precision primary print proc procedure public raiserror read readtext real reconfigure references replication restore restrict return revert revoke rollback rowcount rowguidcol rule save schema securityaudit select set setuser shutdown smalldatetime smallint smallmoney sql_variant statistics table table tablesample text textsize then time timestamp tinyint to top tran transaction trigger truncate try union unique uniqueidentifier update updatetext use user values varbinary varchar varying view waitfor when where while with writetext xml go "
    Public Const SQL_OPERATORS As String = "all and any between cross exists in inner is join left like not null or outer pivot right some unpivot "
    Public Const SQL_FUNCTIONS As String = "ascii char charindex concat concat_ws datalength difference format left len lower ltrim nchar patindex quotename replace replicate reverse right rtrim soundex space str stuff substring translate trim unicode upper abs acos asin atan atn2 avg ceiling count cos cot degrees exp floor log log10 max min pi power radians rand round sign sin sqrt square sum tan current_timestamp dateadd datediff datefromparts datename datepart day getdate getutcdate isdate month sysdatetime year cast coalesce convert current_user iif isnull isnumeric nullif session_user sessionproperty system_user user_name "
    Public Const SQL_OBJECTS As String = "sys objects sysobjects "

    Public Shared Property CurrentStatus As ConnectionStatus
    Public Shared Property CurrentTable As UserTable
    Public Shared Property CurrentEditor As UserEditor
    Public Shared Property CurrentZoom As Integer
    Public Shared Property ConnectionTimeout As Integer = 5
    Public Shared Property CurrentColumnsMode As DataGridViewAutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

    Public Enum ConnectionStatus
        Disconnected = 1
        Connected = 2
        Connecting = 3
    End Enum

    Public Shared Sub AddControlToTab(tab As TabPage, userTable As UserTable)
        tab.Controls?.Clear()
        tab.Controls.Add(userTable)
        userTable.Dock = DockStyle.Fill
        SetCurrentTable(userTable)
    End Sub

    Public Shared Sub AddControlToTab(tab As TabPage, userEditor As UserEditor)
        tab.Controls?.Clear()
        tab.Controls.Add(userEditor)
        userEditor.Dock = DockStyle.Fill
        SetCurrentEditor(userEditor)
    End Sub

    Public Shared Sub SetCurrentTable(userTable As UserTable)
        CurrentTable = userTable
    End Sub

    Public Shared Sub SetCurrentEditor(userEditor As UserEditor)
        CurrentEditor = userEditor
    End Sub

End Class
