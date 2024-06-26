const baseUrl = "http://localhost:5117/";

export async function fetchData(
    url,
    httpMethod = "GET",
    body = null,
    isFileUpload = false,isauth=false ) {
    const headers = {
      Authorization: `Bearer ${localStorage.getItem("authToken")}`,
    };
  
    if (!isFileUpload) {
      headers["Content-Type"] = "application/json";
    }
  
    const response = await fetch(`${baseUrl}${url}`, {
      method: httpMethod,
      headers: headers,
      body: isFileUpload ? body : body ? JSON.stringify(body) : undefined,
    });

    if (response.status == 401 && !isauth) {
      window.location.href = "/Auth/login.html";
      return;
    }
    if (!response.ok) {
      const errorMessage = `Error ${response.status}: ${response.statusText}`;
      throw new Error(errorMessage);
    }
  
    const data = await response.json();
    return data;
  }
