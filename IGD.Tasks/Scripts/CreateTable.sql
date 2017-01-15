IF NOT EXISTS (SELECT * FROM dbo.sysobjects
				WHERE id = object_id(N'dbo.dnn_plTasks_Tasks')
					AND OBJECTPROPERTY(id, N'IsTable') = 1)
	BEGIN
		CREATE TABLE dbo.dnn_plTasks_Tasks(
			TaskId int IDENTITY(1,1) NOT NULL,
			UserId int NOT NULL,
			Name nvarchar(200) NOT NULL,
			[Description] nvarchar(max) NOT NULL,
			IsComplete bit NOT NULL,
			CONSTRAINT PK_dnn_plTasks_Tasks
				PRIMARY KEY CLUSTERED(TaskId ASC)
		)
	END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS
				WHERE TABLE_NAME = object_id(N'dbo.dnn_plTasks_Tasks')
					AND COLUMN_NAME = 'UserId')
	BEGIN
		ALTER TABLE dbo.dnn_plTasks_Tasks
			ADD UserId int NOT NULL
	END
GO