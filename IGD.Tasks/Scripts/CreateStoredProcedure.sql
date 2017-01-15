-- Obtiene todas las tareas
IF EXISTS (SELECT * FROM sys.objects
			WHERE object_id = OBJECT_ID(N'dbo.dnn_plTasks_GetTasks')
				AND type in (N'P', N'PC'))
	DROP PROCEDURE dbo.dnn_plTasks_GetTasks
GO

CREATE PROCEDURE dbo.dnn_plTasks_GetTasks
	@UserId int
AS
	SELECT * FROM dbo.dnn_plTasks_Tasks
	WHERE UserId = @UserId
GO

-- Obtiene una sola tarea
IF EXISTS (SELECT * FROM sys.objects
			WHERE object_id = OBJECT_ID(N'dbo.dnn_plTasks_GetTask')
				AND type in (N'P', N'PC'))
	DROP PROCEDURE dbo.dnn_plTasks_GetTask
GO

CREATE PROCEDURE dbo.dnn_plTasks_GetTask
	@TaskId int
AS
	SELECT * FROM dbo.dnn_plTasks_Tasks
	WHERE TaskId = @TaskId
GO

-- Crea una tarea
IF EXISTS (SELECT * FROM sys.objects
			WHERE object_id = OBJECT_ID(N'dbo.dnn_plTasks_CreateTask')
				AND type in (N'P', N'PC'))
	DROP PROCEDURE dbo.dnn_plTasks_CreateTask
GO

CREATE PROCEDURE dbo.dnn_plTasks_CreateTask
	@Name nvarchar(200),
	@Description nvarchar(max),
	@IsComplete bit,
	@UserId int
AS
	INSERT INTO dbo.dnn_plTasks_Tasks (
		Name,
		[Description],
		IsComplete,
		UserId
	)
	VALUES (
		@Name,
		@Description,
		@IsComplete,
		@UserId
	)

	SELECT @@IDENTITY --Devuelve el Id recién insertado.
GO

-- Actualiza una tarea
IF EXISTS (SELECT * FROM sys.objects
			WHERE object_id = OBJECT_ID(N'dbo.dnn_plTasks_UpdateTasks')
				AND type in (N'P', N'PC'))
	DROP PROCEDURE dbo.dnn_plTasks_UpdateTasks
GO

CREATE PROCEDURE dbo.dnn_plTasks_UpdateTasks
	@TaskId int,
	@Name nvarchar(200),
	@Description nvarchar(max),
	@IsComplete bit
AS
	UPDATE dbo.dnn_plTasks_Tasks
		SET
			Name = @Name,
			[Description] = @Description,
			IsComplete = @IsComplete
		WHERE TaskId = @TaskId
GO

-- Eliminar una tarea
IF EXISTS (SELECT * FROM sys.objects
			WHERE object_id = OBJECT_ID(N'dbo.dnn_plTasks_DeleteTask')
				AND type in (N'P', N'PC'))
	DROP PROCEDURE dbo.dnn_plTasks_DeleteTask
GO

CREATE PROCEDURE dbo.dnn_plTasks_DeleteTask
	@TaskId int
AS
	DELETE FROM dbo.dnn_plTasks_Tasks
	WHERE TaskId = @TaskId
GO