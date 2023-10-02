-- Print the Marathi Table
SELECT * FROM public."MarathiTranslatedWords";

-- Print the row which satisfies the condition
SELECT * FROM public."MarathiTranslatedWords" where "Key" = 'Quote';

-- Check for the Keys which occurred more than once (duplicate Keys)
Select "Key", Count("Key") from public."MarathiTranslatedWords"
group by "Key"
Having Count("Key") > 1