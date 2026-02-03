import { ShortenRequest, ShortenResponse } from "@/types/url";
import { NextRequest, NextResponse } from "next/server";

export async function POST(req: NextRequest) {
  try{  
    const body: ShortenRequest = await req.json();
    const res = await fetch(`${process.env.BACKEND_URL}/shorten`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "accept": "application/json",
      },
      body: JSON.stringify(body),
    });
    if (!res.ok) {
      const errorText = await res.text();
      return NextResponse.json(
        { error: errorText },
        { status: res.status }
      );
    }    
    const data: ShortenResponse = await res.json();
    return NextResponse.json<ShortenResponse>(data);
  } catch (error: any) {
    return NextResponse.json(
      { error: error.message },
      { status: 500 }
    );
  }
}