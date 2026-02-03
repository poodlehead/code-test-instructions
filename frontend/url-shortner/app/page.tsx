"use client";

import { ShortenRequest, ShortenResponse } from "@/types/url";
import { useState } from "react";


export default function Home() {
  const [url, setUrl] = useState("");
  const [alias, setAlias] = useState("");
  const [error, setError] = useState<string | null>(null);
  const [result, setResult] = useState<ShortenResponse | null>(null);
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e: React.SubmitEvent) => {
    e.preventDefault();

    setLoading(true);
    setError(null);
    setResult(null);


    try {
      const payload: ShortenRequest = {
        fullUrl: url,
        customAlias: alias,
      };
      const res = await fetch("/api/shorten", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(payload),
      });

      if (!res.ok) {
        throw new Error("Failed to shorten URL");
      }

      const data = await res.json();
      setResult(data);
    } catch (error: any) {
      setError(error.message);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="flex min-h-screen items-center justify-center bg-zinc-50 font-sans dark:bg-black">
      <main className="flex min-h-screen w-full max-w-3xl flex-col items-center justify-between py-32 px-16 bg-white dark:bg-black sm:items-start">
        <div className="flex flex-col items-center gap-6 text-center sm:items-start sm:text-left">
          <h1 className="max-w-xs text-3xl font-semibold leading-10 tracking-tight text-black dark:text-zinc-50">
            TPX Impact URL Shortner
          </h1>
          <form 
          onSubmit={handleSubmit}
          className="flex w-full max-w-md flex-col gap-4">
            <input
              type="url"
              name="url"
              placeholder="Enter URL to shorten"
              onChange={(e) => setUrl(e.target.value)}
              required
              className="w-full rounded border border-zinc-300 bg-white px-4 py-2 text-black placeholder-zinc-400 shadow-sm focus:border-blue-500 focus:outline-none focus:ring-1 focus:ring-blue-500 dark:border-zinc-700 dark:bg-zinc-900 dark:text-zinc-50 dark:placeholder-zinc-500"
            />
            <input
              type="text"
              name="alias"
              placeholder="Enter a custom alias (optional)"
              onChange={(e) => setAlias(e.target.value)}
              className="w-full rounded border border-zinc-300 bg-white px-4 py-2 text-black placeholder-zinc-400 shadow-sm focus:border-blue-500 focus:outline-none focus:ring-1 focus:ring-blue-500 dark:border-zinc-700 dark:bg-zinc-900 dark:text-zinc-50 dark:placeholder-zinc-500"
            />
            <button
              type="submit"
              disabled={loading}
              className="w-full rounded bg-blue-600 px-4 py-2 text-white hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2"
            >
              {loading? "Creating..." : "Shorten URL"}
            </button>
          </form>
          
          {error && (
            <p className="text-red-600 mt-4">Error: {error}</p>
          )}

          {result && result.alias && (
            <div className="flex flex-col items-center gap-6 text-center sm:items-start sm:text-left mt-4">
              <p className="text-green-600">Shortened URL:</p>
              <a
                href={result.alias}
                target="_blank"
                rel="noopener noreferrer"
                className="text-blue-600 underline"
              >
                {result.alias}
              </a>
            </div>
          )}
        </div>
      </main>
    </div>
  );
}
