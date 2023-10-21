-- Print the Tamil Table
SELECT * FROM public."TamilTranslatedWords";

-- Print the row which satisfies the condition
SELECT * FROM public."TamilTranslatedWords" where "Key" = 'Quote';

-- Check for the Keys which occurred more than once (duplicate Keys)
Select "Key", Count("Key") from public."TamilTranslatedWords"
group by "Key"
Having Count("Key") > 1