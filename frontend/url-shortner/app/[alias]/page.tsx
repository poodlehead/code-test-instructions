import { notFound, redirect } from "next/navigation";

interface PageProps {
  params: {
    alias: string;
  };
}

export default async function AliasPage({ params }: PageProps) {

const res = await fetch(`${process.env.BACKEND_URL}/${params.alias}`,
    {
        redirect: "manual",
        cache: "no-store",
    });
  const data = await res.json();

  if (res.status === 302) {
    const location = res.headers.get("Location");
    if (location) {
      redirect(location);
    }
  }
  else {
    return <div>Error: {data.error}</div>;
  }

  return notFound();
}
