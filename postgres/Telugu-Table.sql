-- Print the Telugu Table
SELECT * FROM public."TeluguTranslatedWords";

-- Print the row which satisfies the condition
SELECT * FROM public."TeluguTranslatedWords" where "Key" = 'Quote';

-- Check for the Keys which occurred more than once (duplicate Keys)
Select "Key", Count("Key") from public."TeluguTranslatedWords"
group by "Key"
Having Count("Key") > 1