---
name: DatabaseInitializer seed data safety
description: Embedding markdown code fences (triple backticks) or raw SQL inside C# verbatim/raw string literals breaks the compiler. Use parameterized inserts instead.
---

## Rule
Never embed multi-line blog/article content with markdown code fences directly inside C# verbatim string literals passed to `ExecuteAsync`. Use parameterized Dapper inserts where the content is a C# string variable.

**Why:** Triple backtick strings (markdown code blocks) inside `@"..."` verbatim strings confuse the C# parser, causing CS1056/CS8997 unterminated raw string literal errors.

**How to apply:** In `DatabaseInitializer.SeedBlogPostsAsync`, blog post content is built as C# string concatenation and passed as a Dapper anonymous parameter object — never interpolated into the SQL string.
