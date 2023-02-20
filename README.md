# Hangfire Test (Proof of Concept)

This project is a proof of concept to demonstrate a long-running SQL stored procedure, that is invoked via Web API and managed through Hangfire.

## Setup

To set up this project, create this stored procedure on a local SQL Server instance:

```sql
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[sp_SlowRunningProcess]
as
begin
	set nocount on;

	-- This command will pause execution for 1 minute
	waitfor delay '00:01:00';

	-- This will return a pseudo-random number (i.e. the milliseconds on the current time)
	select datepart(millisecond, getutcdate());
end
GO
```

Then update the connection string in web.config to point to your database.

## Running the Project

When you first load the project, you should be taken to the Swagger page.

To kick off a long-running process, make a request to the `POST /Process` endpoint. The response will contain a job id.

To monitor the job, make a request to `GET /Jobs/{id}`. The response will contain a "History" collection, and the latest event has a property named "StateName". This is the status of the job. While the job is running, the StateName will be "Processing". When it is complete, the Statename will change to "Succeeded."