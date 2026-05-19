from sqlalchemy import Column, String, Integer, ForeignKey, Date
from sqlalchemy.orm import relationship

from app.database.connection import Base

class Chapter(Base):
    __tablename__ = "chapter"

    id = Column(Integer, primary_key=True)
    manga_id = Column(Integer, ForeignKey("manga.id"))
    title = Column(String)
    number = Column(String)
    release_date = Column(Date)

    # RELAÇÕES

    # N:1 -> Chapter -> Manga
    manga = relationship(
        "Manga",
        back_populates="chapters"
    )

    # 1:N -> Chapter -> Pages
    pages = relationship(
        "Page",
        back_populates="chapter",
        cascade="all, delete-orphan"
    )