USE [海口8#.mdb]
GO

--|--------------------------------------------------------------------------------
--| [Meta_FCMasterInsert] - Insert Procedure Script for Meta_FCMaster
--|--------------------------------------------------------------------------------
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id (N'[dbo].[Meta_FCMasterInsert]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1) DROP PROCEDURE [dbo].[Meta_FCMasterInsert]
GO

CREATE PROCEDURE [dbo].[Meta_FCMasterInsert]
(
	@FunctionName Text = NULL,
	@FunctionCode Long = NULL,
	@Description Text = NULL,
	@Function Memo = NULL,
	@DIAG Byte = NULL,
	@InputCount Byte = NULL,
	@SpecCount Byte = NULL,
	@OutPutCount Byte = NULL,
	@InternalCount Long = NULL,
	@FCLength Integer = NULL,
	@Type Text = NULL
)
AS
	SET NOCOUNT ON

	INSERT INTO [Meta_FCMaster]
	(
		[FunctionName],
		[FunctionCode],
		[Description],
		[Function],
		[DIAG],
		[InputCount],
		[SpecCount],
		[OutPutCount],
		[InternalCount],
		[FCLength],
		[Type]
	)
	VALUES
	(
		@FunctionName,
		@FunctionCode,
		@Description,
		@Function,
		@DIAG,
		@InputCount,
		@SpecCount,
		@OutPutCount,
		@InternalCount,
		@FCLength,
		@Type
	)

	RETURN @@Error
GO

--|--------------------------------------------------------------------------------
--| [Meta_FCMasterUpdate] - Update Procedure Script for Meta_FCMaster
--|--------------------------------------------------------------------------------
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id (N'[dbo].[Meta_FCMasterUpdate]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1) DROP PROCEDURE [dbo].[Meta_FCMasterUpdate]
GO

CREATE PROCEDURE [dbo].[Meta_FCMasterUpdate]
(
	@FunctionName Text = NULL,
	@FunctionCode Long = NULL,
	@Description Text = NULL,
	@Function Memo = NULL,
	@DIAG Byte = NULL,
	@InputCount Byte = NULL,
	@SpecCount Byte = NULL,
	@OutPutCount Byte = NULL,
	@InternalCount Long = NULL,
	@FCLength Integer = NULL,
	@Type Text = NULL
)
AS
	SET NOCOUNT ON
	
	UPDATE [Meta_FCMaster]
	SET
		[FunctionName] = @FunctionName,
		[FunctionCode] = @FunctionCode,
		[Description] = @Description,
		[Function] = @Function,
		[DIAG] = @DIAG,
		[InputCount] = @InputCount,
		[SpecCount] = @SpecCount,
		[OutPutCount] = @OutPutCount,
		[InternalCount] = @InternalCount,
		[FCLength] = @FCLength,
		[Type] = @Type
	WHERE 
		[FunctionName] = @FunctionName

	RETURN @@Error
GO

--|--------------------------------------------------------------------------------
--| [Meta_FCMasterDelete] - Update Procedure Script for Meta_FCMaster
--|--------------------------------------------------------------------------------
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id (N'[dbo].[Meta_FCMasterDelete]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1) DROP PROCEDURE [dbo].[Meta_FCMasterDelete]
GO

CREATE PROCEDURE [dbo].[Meta_FCMasterDelete]
(
	@FunctionName Text
)
AS
	SET NOCOUNT ON

	DELETE 
	FROM   [Meta_FCMaster]
	WHERE  
		[FunctionName] = @FunctionName

	RETURN @@Error
GO

