-- Step 1: Create Database
CREATE DATABASE StudentSyncDatabase;
GO

USE StudentSyncDatabase;
GO

-- Step 2: Create Source Table with Timestamp
CREATE TABLE SourceStudentTable (
    StudentId INT PRIMARY KEY IDENTITY(1,1),
    StudentName NVARCHAR(255),
    Class NVARCHAR(50),
    Grade NVARCHAR(10),
    IsDeleted BIT DEFAULT 0, -- Soft delete flag
    Timestamp DATETIME DEFAULT GETDATE() -- Timestamp for record creation or update
);
GO

-- Step 3: Create Target Table with Timestamp
CREATE TABLE TargetStudentTable (
    TargetStudentId INT PRIMARY KEY IDENTITY(1,1),
    StudentId INT, -- Foreign key to SourceStudentTable
    StudentName NVARCHAR(255),
    Class NVARCHAR(50),
    Grade NVARCHAR(10),
    Timestamp DATETIME -- Timestamp for record creation or update
);
GO

-- Step 4: Create ETL Procedure for Synchronizing Data
CREATE PROCEDURE ETL_SyncStudentData
AS
BEGIN
    -- Step 1: Extract - Get all records from SourceStudentTable
    DECLARE @SourceTable TABLE (
        StudentId INT,
        StudentName NVARCHAR(255),
        Class NVARCHAR(50),
        Grade NVARCHAR(10),
        IsDeleted BIT,
        Timestamp DATETIME
    );

    -- Extract data from SourceStudentTable
    INSERT INTO @SourceTable
    SELECT StudentId, StudentName, Class, Grade, IsDeleted, Timestamp
    FROM SourceStudentTable;

    -- Step 2: Transform - Handle Soft Deletes
    -- Remove soft-deleted records from the TargetStudentTable
    DELETE FROM TargetStudentTable
    WHERE StudentId IN (SELECT StudentId FROM @SourceTable WHERE IsDeleted = 1);

    -- Step 3: Load - Insert new records into TargetStudentTable
    INSERT INTO TargetStudentTable (StudentId, StudentName, Class, Grade, Timestamp)
    SELECT StudentId, StudentName, Class, Grade, Timestamp
    FROM @SourceTable
    WHERE StudentId NOT IN (SELECT StudentId FROM TargetStudentTable)
    AND IsDeleted = 0;

    -- Update existing records in TargetStudentTable
    UPDATE TargetStudentTable
    SET StudentName = s.StudentName,
        Class = s.Class,
        Grade = s.Grade,
        Timestamp = s.Timestamp
    FROM TargetStudentTable t
    JOIN @SourceTable s ON t.StudentId = s.StudentId
    WHERE s.IsDeleted = 0;
END;
GO

-- Step 5: Insert Entries into SourceStudentTable
INSERT INTO SourceStudentTable (StudentName, Class, Grade)
VALUES 
    ('John Doe', '10th Grade', 'A'),
    ('Jane Smith', '11th Grade', 'B'),
    ('Emily Davis', '12th Grade', 'C'),
    ('Michael Brown', '10th Grade', 'B'),
    ('Sarah Wilson', '11th Grade', 'A');
GO

-- Step 6: Run the ETL Procedure to Synchronize Data
EXEC ETL_SyncStudentData;
GO

-- Perform a soft delete by updating the IsDeleted flag
UPDATE SourceStudentTable
SET IsDeleted = 1
WHERE StudentName = 'John Doe';

-- Re-run the ETL procedure to synchronize data after the soft delete
EXEC ETL_SyncStudentData;

-- Check the results in both tables after synchronization
SELECT * FROM SourceStudentTable;
SELECT * FROM TargetStudentTable;

-- Query to display the latest 3 records based on the Timestamp
SELECT TOP 3 *
FROM TargetStudentTable
ORDER BY Timestamp DESC;