import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import CardMedia from "@mui/material/CardMedia";
import Typography from "@mui/material/Typography";
import { GetInforUser, UploadFile } from "../api/userApi";
import { useState, useEffect } from "react";
import { useLocation } from "react-router-dom";
import { Box, Divider, Button, styled } from "@mui/material";
import CloudUploadIcon from "@mui/icons-material/CloudUpload";
import Notify from "../components/Notify";

const VisuallyHiddenInput = styled("input")({
  clip: "rect(0 0 0 0)",
  clipPath: "inset(50%)",
  height: 1,
  overflow: "hidden",
  position: "absolute",
  bottom: 0,
  left: 0,
  whiteSpace: "nowrap",
  width: 1,
});

export default function InformationUser() {
  const location = useLocation();
  const [file, setFile] = useState({});
  const [user, setUser] = useState({});
  const [alert, setAlert] = useState({
    message: "",
    severity: "",
    open: false,
  });
  const getInfor = async () => {
    try {
      const searchParams = new URLSearchParams(location.search);
      const paramValue = searchParams.get("username");
      const user = await GetInforUser(paramValue);
      console.log(user);
      setUser(user);
    } catch (error) {
      console.log(error);
    }
  };

  const upLoadFile = async (event) => {
    const formData = new FormData();
    formData.append("file", event.target.files[0]);
    try {
      const res = await UploadFile(formData, {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      });
      setFile(res);
      setAlert({
        message: "Upload file successfully",
        severity: "success",
        open: true,
      });
    } catch (error) {
      console.log(error);
      setAlert({
        message: "Upload file Error",
        severity: "error",
        open: true,
      });
    }
  };

  useEffect(() => {
    getInfor();
  }, []);
  return (
    <>
      <Card sx={{ display: "flex", flexDirection: "row", padding: "10px" }}>
        <CardContent sx={{ width: 200, height: 250 }}>
          <CardMedia
            sx={{
              objectFit: "contain",
              width: "90%",
              height: "90%",
              borderRadius: 2,
              marginBottom: 1,
            }}
            image={user.avatar}
            title="green iguana"
          />
          <Typography gutterBottom variant="h5" component="div">
            {user.name}
          </Typography>
        </CardContent>
        <CardContent sx={{ width: 500 }}>
          <Typography gutterBottom variant="h5" component="div">
            Information
          </Typography>
          <Divider sx={{ marginBottom: 2 }} />
          <Box display="flex" flexDirection="column" alignItems="self-start">
            <Box display="flex" flexDirection="row" gap={10}>
              <Box>
                <Typography gutterBottom variant="h6" component="div">
                  Address
                </Typography>
                <Typography variant="body2" color="text.secondary">
                  {user.address}
                </Typography>
              </Box>
              <Box>
                <Typography gutterBottom variant="h6" component="div">
                  Gender
                </Typography>
                <Typography variant="body2" color="text.secondary">
                  {user.gender ? "Male" : "Female"}
                </Typography>
              </Box>
              <Box>
                <Typography gutterBottom variant="h6" component="div">
                  Email
                </Typography>
                <Typography variant="body2" color="text.secondary">
                  {user.email}
                </Typography>
              </Box>
            </Box>
          </Box>
        </CardContent>
      </Card>
      <Box>
        <Button
          component="label"
          role={undefined}
          variant="contained"
          tabIndex={-1}
          startIcon={<CloudUploadIcon />}
          sx={{ marginTop: 10 }}
        >
          Upload file
          <VisuallyHiddenInput type="file" onChange={upLoadFile} />
        </Button>
        <Box
          flexDirection="row"
          display="flex"
          justifyContent="center"
          alignItems="center"
        >
          <Typography variant="h6">Version: </Typography>
          <Typography sx={{ marginLeft: 2 }}>{file?.version}</Typography>
        </Box>
        <Box
          flexDirection="row"
          display="flex"
          justifyContent="center"
          alignItems="center"
        >
          <Typography variant="h6">Url: </Typography>
          <Typography sx={{ marginLeft: 2 }}>
            {file.url?.split("/").pop()}
          </Typography>
        </Box>
        <Notify
          open={alert.open}
          message={alert.message}
          severity={alert.severity}
          onclose={() =>
            setAlert({
              message: "",
              severity: "",
              open: false,
            })
          }
        />
      </Box>
    </>
  );
}
