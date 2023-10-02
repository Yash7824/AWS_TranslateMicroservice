-- Print the Hindi Table
SELECT * FROM public."HindiTranslatedWords";

-- Print the row which satisfies the condition
SELECT * FROM public."HindiTranslatedWords" where "Key" = 'Quote';

-- Check for the Keys which occurred more than once (duplicate Keys)
Select "Key" from public."HindiTranslatedWords"
group by "Key"
Having Count("Key") > 1