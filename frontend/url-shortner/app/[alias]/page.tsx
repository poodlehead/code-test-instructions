import { notFound, redirect } from "next/navigation";

interface PageProps {
  params: {
    alias: string;
  };
}

export default async function AliasPage({ params }: PageProps) {
  
  const {alias} = await params;
  console.log("Environment BACKEND_URL:", `${process.env.BACKEND_URL}/${alias}`);
  const res = await fetch(`${process.env.BACKEND_URL}/${alias}`,
    {
        redirect: "manual",
        cache: "no-store",
    });
  console.log("Response status from backend:", res.status);
  

  if (res.status === 302) {
    const location = res.headers.get("Location");
    if (location) {
      redirect(location);
    }
  }
  else {
    return <div>Error: {res.statusText}</div>;
  }

  return notFound();
}
