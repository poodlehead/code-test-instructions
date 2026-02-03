export interface ShortenRequest {
  fullUrl: string;
  customAlias: string | null;
}

export interface ShortenResponse {
  alias: string;
}
