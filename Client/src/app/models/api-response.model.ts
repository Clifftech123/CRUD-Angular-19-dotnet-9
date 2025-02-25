
export interface ApiResponse<T> {
  
 success: boolean;
  message: string;
  data: T;
  statusCode: number;
  totalCount?: number;
}