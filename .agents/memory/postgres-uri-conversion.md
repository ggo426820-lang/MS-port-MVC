---
name: PostgreSQL URI conversion
description: Replit's DATABASE_URL is a URI (postgresql://user:pass@host/db) that NpgsqlConnectionStringBuilder cannot parse directly. Must convert before passing to EF Core or Dapper.
---

## Rule
Always convert `DATABASE_URL` through `ConnectionHelper.ToNpgsqlConnectionString()` before using it anywhere in the app (EF Core `UseNpgsql()`, `DbConnectionFactory`, etc.).

**Why:** `NpgsqlConnectionStringBuilder` parses key=value strings, not URIs. Passing the raw URI causes a `KeyNotFoundException` inside Npgsql internals on startup.

**How to apply:** `ConnectionHelper` lives in `MostafaSaidPortfolio/Data/ConnectionHelper.cs`. Both `Program.cs` and `DbConnectionFactory` call it at startup. If adding new database consumers, call the same helper.
