
CREATE UNIQUE INDEX IX_Users_Email ON Users (Email);

CREATE INDEX IX_Users_Name ON Users (Name);

CREATE INDEX IX_Users_IsActive ON Users (IsActive);

CREATE INDEX IX_Users_IsActive_Name ON Users (IsActive, Name);

CREATE INDEX IX_Users_IsActive_Email ON Users (IsActive, Email);


SELECT Id, Name, Email, IsActive, CreatedAt 
FROM Users 
WHERE IsActive = 1 
  AND (LOWER(Name) LIKE '%john%' OR LOWER(Email) LIKE '%john%')
ORDER BY Name;


SELECT COUNT(*) 
FROM Users 
WHERE IsActive = 1 
  AND (LOWER(Name) LIKE '%search%' OR LOWER(Email) LIKE '%search%');


SELECT 
    i.name AS IndexName,
    i.type_desc AS IndexType,
    STRING_AGG(c.name, ', ') WITHIN GROUP (ORDER BY ic.key_ordinal) AS IndexColumns
FROM sys.indexes i
INNER JOIN sys.index_columns ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id
INNER JOIN sys.columns c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
WHERE i.object_id = OBJECT_ID('Users')
GROUP BY i.name, i.type_desc
ORDER BY i.name;
