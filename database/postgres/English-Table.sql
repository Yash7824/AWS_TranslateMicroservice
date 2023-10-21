-- Print the English Table
SELECT * FROM public."EnglishTranslatedWords";

-- Print the row which satisfies the condition
SELECT * FROM public."EnglishTranslatedWords" where "Key" = 'Quote';

-- Check for the Keys which occurred more than once (duplicate Keys)
Select "Key" from public."EnglishTranslatedWords"
group by "Key"
Having Count("Key") > 1

Select * from public."EnglishTranslatedWords" where "Key" != "Value";