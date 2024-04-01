import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import CardMedia from "@mui/material/CardMedia";
import Typography from "@mui/material/Typography";
import { GetInforUser } from "../api/userApi";
import { useState, useEffect } from "react";
import { useLocation } from "react-router-dom";
import { Box, Divider } from "@mui/material";

export default function InformationUser() {
  const location = useLocation();
  const [user, setUser] = useState({});
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

  useEffect(() => {
    getInfor();
  }, []);
  return (
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
  );
}
